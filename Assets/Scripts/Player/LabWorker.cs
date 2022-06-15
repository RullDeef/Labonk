using UnityEngine;

/*
    Класс для контроля процесса скатывания и сдачи лабы.

    Для того чтобы сдать лабу, необходимо сначала подготовить ее. Для этого
        надо подойти к рабочему месту (столу), и если возле него в определенном
            радиусе будет стул, то игрок подтягивает к себе стул и садится катать.
                Также необходимо, чтобы на нем был ноут, или он был в руках игрока.

        Если хотя бы одно из этих условий не выполнено - прес эф.
    
    После скатывания лабы, игрок тут же встает со стула держа ноут в руках.
        После этого необходимо как можно скорее добежать до препода и взаимодействовать
            с ним, держа ноут в руках. Далее наступает процесс показа.

    Процесс показа не могут прервать другие игроки. (а вот катание - могут, хехе)
        Но он может быть прерван внезапным анекдотом от самого препода.
            В этом случае у других студентов есть шанс отбить занятое место. 
*/

public class LabWorker : MonoBehaviour
{
    public enum State
    {
        LAB_NOT_PREPARED,
        MAKING_LAB,
        LAB_PREPARED,
        LAB_SHOWING,
        LAB_DONE
    }

    public State CurrentState { get; private set; } = State.LAB_NOT_PREPARED;

    [SerializeField] private Transform workSpotHolder;
    [SerializeField] private Transform chairHolder;
    [SerializeField] private float minStartWorkingDistance = 2.0f;
    [SerializeField] private float minChairDistance = 2.0f;

    public void SitForPreparing()
    {
        if (CurrentState == State.LAB_NOT_PREPARED)
        {
            Transform chair = GetNearestChair(out float distance);
        }
        else
        {
            Debug.Log("invalid state for SitForPreparing transition");
        }
    }

    public void CatchTutor()
    {
        if (CurrentState == State.LAB_PREPARED)
        {

        }
        else
        {
            Debug.Log("invalid state for CatchTutor");
        }
    }

    private Transform GetNearestChair(out float distance)
    {
        Transform nearest = chairHolder.GetChild(0);
        float minSqrDistance = (nearest.position - transform.position).sqrMagnitude;

        for (int i = 1; i < chairHolder.childCount; i++)
        {
            Transform currChair = chairHolder.GetChild(i);
            float currDist = (currChair.position - transform.position).sqrMagnitude;

            if (currDist < minSqrDistance)
            {
                nearest = currChair;
                minSqrDistance = currDist;
            }
        }

        distance = Mathf.Sqrt(minSqrDistance);
        return nearest;
    }
}
