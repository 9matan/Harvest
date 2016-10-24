using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour {

	static public GameController i {
		get { return _instance; }
	}
	static private GameController _instance;

	public int scoreAtAll {
		get { return _scoreAtAll; }
	}

	public int bestScore
	{
		get
		{
			if (!_bestScore.isInit)
				_bestScore.Initialize();
			return _bestScore.GetInt();
        }

		set
		{
			if (!_bestScore.isInit)
				_bestScore.Initialize();
			_bestScore.Set(value);
			_bestScore.Save();
        }
	}

	public int Score_soul;
    public Text Soul_text;
    public skillscontroll[] mas_skills;

	[SerializeField]
	protected Text _score_text;
	[SerializeField]
	protected DOEncryptRegistry _bestScore; 

	protected int _scoreAtAll = 0;

	[Header("Timer")]
	public int			level_duration;
	protected DateTime	starttime;
	public Image		time_progress;

	void Awake()
	{
		_instance = this;
    }

	void Start ()
	{
		gameOver = false;
		starttime = DateTime.Now;

		_instance = this;
        Change_soul_count(0);

		if (PlayerPrefs.GetInt("sound", 1) == 1)
            AudioListener.volume = 1f;
        else
            AudioListener.volume = 0;
    }

    public void Change_soul_count(int a)
    {
		if (a > 0)
		{
			this._UpdateCombo();
            _scoreAtAll += a * _combo;
			bestScore = Math.Max(bestScore, _scoreAtAll);
        }

		Score_soul = Math.Max(0, a + Score_soul);
        Soul_text.text = Score_soul.ToString();
		_score_text.text = _scoreAtAll.ToString();
        for (int i = 0; i < mas_skills.Length; i++)
        {
            if (mas_skills[i].price > Score_soul)
                mas_skills[i].no_money.SetActive(true);
            else
                mas_skills[i].no_money.SetActive(false);
        }
    }

	protected float _gameTime = 0.0f;

	// Update is called once per frame
	void Update ()
	{
		_gameTime += Time.deltaTime;
		if (_gameTime <= (float)level_duration)
			time_progress.fillAmount = 1 - ((float)_gameTime / (float)level_duration);
		else
		{
			this._GameOver();
		}

		this._CheckCombo();
    }

	[SerializeField]
	protected Animator _canvasAnimator;
	public void Damage(int souls)
	{
		this.Change_soul_count(-souls);
		_canvasAnimator.Play("Damage");
    }

	//
	// < Combo >
	//

	[Header("Combo")]
	[SerializeField]
	protected float _comboDeltaTime = 2.0f;
	[SerializeField]
	protected Text	_comboText;

	protected int	_combo = 1;
	protected float _lastTime = 0.0f;

	protected void _UpdateCombo()
	{
		if(Time.realtimeSinceStartup - _lastTime <= _comboDeltaTime)
		{
			++_combo;
			_comboText.text = "x" + _combo.ToString();
        }
		else
		{
			_comboText.text = "";
			_combo = 1;
		}

		_lastTime = Time.realtimeSinceStartup;
    } 

	protected void _CheckCombo()
	{
		if(Time.realtimeSinceStartup - _lastTime > _comboDeltaTime)
			_comboText.text = "";
	}

	//
	// </ Combo >
	//

	[Header("Game over")]
	[SerializeField]
	protected GameObject _gameOverObject;

	public bool gameOver { get; protected set; }

	protected void _GameOver()
	{
		gameOver = true;
        _gameOverObject.Show();
    }

}
