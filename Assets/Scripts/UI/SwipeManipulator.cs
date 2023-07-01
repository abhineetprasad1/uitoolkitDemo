using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SwipeDirection {
    Up,
    Down
}


public class SwipeManipulator : PointerManipulator
{

    public event System.Action<Vector3> OnTouchMove;
    public event System.Action<SwipeDirection> OnSwipe; //TODO: passs swipe speed param to affect the slide swipe animation duration
    public event System.Action OnSwipeCancel;



    private float swipeThreshold = 10; //TODO: encapsulate this in Swipe Config, make it editable in editor and pass it along to SwipeManipulator
    private float swipeSpeedThreshold = 200;//TODO: same as above
    private Vector3 pointerStartPosition;
    private float touchStartTime;
    private bool isSwiping = false;


    public SwipeManipulator(VisualElement target)
    {
        this.target = target;
    }


    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<PointerDownEvent>(PointerDownHandler);
        target.RegisterCallback<PointerMoveEvent>(PointerMoveHandler);
        target.RegisterCallback<PointerUpEvent>(PointerUpHandler);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<PointerDownEvent>(PointerDownHandler);
        target.UnregisterCallback<PointerMoveEvent>(PointerMoveHandler);
        target.UnregisterCallback<PointerUpEvent>(PointerUpHandler);
    }

    private void PointerDownHandler(PointerDownEvent evt)
    {
        pointerStartPosition = evt.position;
        target.CapturePointer(evt.pointerId);
        touchStartTime = Time.time;
        isSwiping = true;

    }

    private void PointerMoveHandler(PointerMoveEvent evt)
    {
        if(isSwiping && target.HasPointerCapture(evt.pointerId))
        {
            Vector3 totalMoveDelta = evt.position - pointerStartPosition;
            OnTouchMove?.Invoke(totalMoveDelta);
        }
    }

    private void PointerUpHandler(PointerUpEvent evt)
    {
        if(isSwiping && target.HasPointerCapture(evt.pointerId))
        {

            target.ReleasePointer(evt.pointerId);
            isSwiping = false;

            float timeDelta = Time.time - touchStartTime;
            float swipeDelta = evt.position.y - pointerStartPosition.y;
            float swipeSpeed = Mathf.Abs(swipeDelta)/ timeDelta;

            
            bool isItASwipe = Mathf.Abs(swipeDelta) > swipeThreshold;
            bool isItFastEnoughToChangeSlide = isItASwipe && swipeSpeed > swipeSpeedThreshold;

            var targetHeight = target.resolvedStyle.height;
            var thresholdHeight = 0.4f * targetHeight;
            bool didMoveMoreThanThresholdHeight = Mathf.Abs(swipeDelta) > thresholdHeight;

            if (isItFastEnoughToChangeSlide || didMoveMoreThanThresholdHeight)
            {
                SwipeDirection swipeDir = Mathf.Sign(swipeDelta) > 0 ? SwipeDirection.Down : SwipeDirection.Up;
                OnSwipe?.Invoke(swipeDir);
            }
            else
            {
                OnSwipeCancel?.Invoke();
            }

        }
    }

}
