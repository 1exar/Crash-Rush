using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PreviewBallsAnimationController : MonoBehaviour
{

    [SerializeField] private List<Entity> _firstCamera;
    [SerializeField] private List<Entity> _secondCamera;
    [SerializeField] private List<Entity> _thirdCamera;

    public void ShowBall(int index, EntityType type)
    {
        switch (index)
        {
            case 0:
                SelectBall(type, _firstCamera);
                break;
            case 1:
                SelectBall(type, _secondCamera);
                break;
            case 2:
                SelectBall(type, _thirdCamera);
                break;
        }
    }
    
    private void SelectBall(EntityType type, List<Entity> balls)
    {
        balls.ForEach(entity =>
        {
            entity.gameObject.SetActive(false);
            if (entity.CurrentType == type)
            {
                entity.gameObject.SetActive(true);
            }
        });
    }

}
