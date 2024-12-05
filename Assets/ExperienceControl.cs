using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceControl : MonoBehaviour
{
    public GameObject Target;

    // Rotation settings
    private float rotationSpeed = 0.5f;
    private Vector2 lastTouchPosition;
    private Vector2 lastMousePosition;
    private bool isDragging = false;
    
    // Scale settings
    private float minScale = 0.5f;
    private float maxScale = 2.0f;
    private float currentScale = 1.0f;
    private float scaleSpeed = 5.0f;
    private Vector3 smoothVelocity;

    void Update()
    {
        // Handle touch inputs on mobile
        if (Input.touchCount > 0)
        {
            Debug.Log($"Touch Count: {Input.touchCount}");
            
            if (Input.touchCount == 1) // Single touch for rotation
            {
                Touch touch = Input.GetTouch(0);
                Debug.Log($"Touch Phase: {touch.phase}, Position: {touch.position}");

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        lastTouchPosition = touch.position;
                        Debug.Log("Touch Began at: " + lastTouchPosition);
                        break;

                    case TouchPhase.Moved:
                        Vector2 deltaPosition = touch.position - lastTouchPosition;
                        Debug.Log($"Delta Movement: {deltaPosition}");
                        // Rotate the target based on horizontal swipe
                        Target.transform.Rotate(Vector3.up, -deltaPosition.x * rotationSpeed);
                        lastTouchPosition = touch.position;
                        break;
                }
            }
            else if (Input.touchCount == 2) // Two finger touch for scaling
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);

                Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
                Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
                
                float prevTouchDeltaMag = (touch0PrevPos - touch1PrevPos).magnitude;
                float touchDeltaMag = (touch0.position - touch1.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                Debug.Log($"Pinch Delta: {deltaMagnitudeDiff}");

                float scaleFactor = deltaMagnitudeDiff * 0.01f;
                currentScale = Mathf.Clamp(currentScale - scaleFactor, minScale, maxScale);
                Debug.Log($"Current Scale: {currentScale}");

                Vector3 targetScale = Vector3.one * currentScale;
                Target.transform.localScale = Vector3.SmoothDamp(
                    Target.transform.localScale, 
                    targetScale, 
                    ref smoothVelocity, 
                    1 / scaleSpeed
                );
            }
        }
        // Handle mouse input for editor testing
        else
        {
            // Mouse drag for rotation
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                lastMousePosition = Input.mousePosition;
                Debug.Log("Mouse Down at: " + lastMousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                Debug.Log("Mouse Up");
            }

            if (isDragging)
            {
                Vector2 deltaPosition = (Vector2)Input.mousePosition - lastMousePosition;
                Debug.Log($"Mouse Delta: {deltaPosition}");
                Target.transform.Rotate(Vector3.up, -deltaPosition.x * rotationSpeed);
                lastMousePosition = Input.mousePosition;
            }

            // Mouse wheel for scaling
            float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
            if (scrollDelta != 0)
            {
                Debug.Log($"Scroll Delta: {scrollDelta}");
                currentScale = Mathf.Clamp(currentScale + scrollDelta, minScale, maxScale);
                Debug.Log($"Current Scale: {currentScale}");

                Vector3 targetScale = Vector3.one * currentScale;
                Target.transform.localScale = Vector3.SmoothDamp(
                    Target.transform.localScale, 
                    targetScale, 
                    ref smoothVelocity, 
                    1 / scaleSpeed
                );
            }
        }
    }
}
