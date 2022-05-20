using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private BallView _view;
    [SerializeField]
    private BallMovment _movment;
    [SerializeField]
    private BallHealth _health;
    [SerializeField]
    private BallAttack _attack;
    private bool _isMine;

    public bool IsMine
    {
        get { return _isMine; }
        protected set {}
    }
    
    public BallMovment Movment
    {
        get { return _movment; }
        protected set {}
    }
    
    public BallView View
    {
        get { return _view; }
        protected set {}
    }

    public BallAttack Attack
    {
        get { return _attack; }
        protected set {}
    }

    public BallHealth Health
    {
        get { return _health; }
        protected set {}
    }
    
    public void Init(bool _isMine, BallsContainer_old _container)
    {
        _view.SetCircleColor(_isMine);
        _movment.Init(_isMine, _container);
        this._isMine = _isMine;
        _attack.Init(_isMine);
        _health.Init(this, _container);
    }

    public void MakeTurn()
    {
        _view.ChoiseMe();
        _movment.MyTurn();
        _attack.MyTurn();
    }

    public void MyTeamAttack()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        _attack.MyTeamAttack();
    }

    public void EnemyTeamAttack()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        _attack.EnemyTeamAttack();
    }
    
    public void GetDamage(int damage)
    {
        _health.GetDamage(damage);
    }

    public bool isMoving()
    {
        return _movment.isMoving();
    }
    
}