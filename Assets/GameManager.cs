using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private List<PlayerObject> _players = new List<PlayerObject>();
    private List<PlayerObject> _players2 = new List<PlayerObject>();
    private List<MonoBehaviour> _objects = new List<MonoBehaviour>();
    private List<EnemyObject> _enemies = new List<EnemyObject>();
    private List<BoxObject> _boxes = new List<BoxObject>();
    private CatObject cat;
    private CatBedObject catBed;
    private int playerWeight = 0;
    private PlayerObject _mainPlayer;
    private bool _won = false;
    private const int leftX = 2;
    private const int downY = 2;
    private const int rightX = 13;
    private const int upY = 9;
    public bool Lost { get; set; }
    AudioSource audioData;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }


    void Update()
    {
        MovePlayers();
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void MovePlayers()
    {
        if(_won)
            return;
        Vector3 moveDir = default;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDir = new Vector3(-1, 0);
            _mainPlayer.animator.Play("Left");
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDir = new Vector3(0, 1);
            _mainPlayer.animator.Play("Up");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDir = new Vector3(0, -1);
            _mainPlayer.animator.Play("Down");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDir = new Vector3(1, 0);
            _mainPlayer.animator.Play("Right");
        }


        if (moveDir == default)
        {
            return;
        }

        bool catMove = false;
        foreach (var player in _players)
        {
            var playerPos = player.transform.position;
            Vector3 newPos = new Vector3(playerPos.x + moveDir.x, playerPos.y + moveDir.y);
            if (newPos.x < leftX || newPos.x > rightX || newPos.y < downY || newPos.y > upY)
            {
                return;
            }

            if (newPos.x == cat.transform.position.x && newPos.y == cat.transform.position.y)
            {
                if (playerWeight >= cat.Weight)
                {
                    catMove = true;
                    Vector3 catNewPos = new Vector3(cat.transform.position.x + moveDir.x,
                        cat.transform.position.y + moveDir.y);

                    if (catNewPos.x < leftX || catNewPos.x > rightX || catNewPos.y < downY || catNewPos.y > upY)
                    {
                        return;
                    }

                    foreach (var o in _boxes)
                    {
                        var blockPosition = o.transform.position;
                        if (catNewPos.x == blockPosition.x && catNewPos.y == blockPosition.y)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    return;
                }
            }

            foreach (var o in _boxes)
            {
                var blockPosition = o.transform.position;
                if (newPos.x == blockPosition.x && newPos.y == blockPosition.y)
                {
                    return;
                }
            }
            

            foreach (var enemy in _enemies)
            {
                var enemyPosition = enemy.transform.position;
                if (newPos.x == enemyPosition.x && newPos.y == enemyPosition.y)
                {
                    LoseLevel();
                    return;
                }
            }
        }

        if (catMove)
        {
            var position = cat.transform.position;
            position = new Vector3(position.x + moveDir.x, position.y + moveDir.y);
            cat.transform.position = position;
            foreach (var enemy in _enemies)
            {
                var enemyPosition = enemy.transform.position;
                if (position.Equals(enemyPosition))
                {
                    Destroy(cat.gameObject);
                    _mainPlayer.animator.Play("Lose");
                    Invoke(nameof(RestartLevel),1);
                }
            }
            if (position.x == catBed.transform.position.x && position.y == catBed.transform.position.y)
            {
                WinLevel();
            }
        }

        foreach (var player in _players)
        {
            var playerPos = player.transform.position;
            Vector3 newPos = new Vector3(playerPos.x + moveDir.x, playerPos.y + moveDir.y);
            player.transform.position = newPos;
        }
    }

    private void LoseLevel()
    {
        foreach (var player in _players)
        {
            Destroy(player.gameObject);
        }
        if(!Lost)
            Invoke(nameof(RestartLevel),1);
        Lost = true;

    }


    private void WinLevel()
    {
        _mainPlayer.animator.Play("Win");
        audioData.Play(0);
        _won = true;
        Invoke(nameof(GoToNextLevel),3);
    }

    private void GoToNextLevel()
    {
        SceneManager.LoadScene("Level" + (int.Parse(SceneManager.GetActiveScene().name.Replace("Level","")) + 1));
    }


    public void AddPlayer(PlayerObject playerObject)
    {
        if (_players.Count == 0)
        {
            _mainPlayer = playerObject;
        }
        _players.Add(playerObject);
        _objects.Add(playerObject);
        playerWeight++;
    }

    public void AddBlock(BoxObject boxObject)
    {
        _boxes.Add(boxObject);
        _objects.Add(boxObject);
    }

    public bool IsEmpty(Vector3 newPos)
    {
        foreach (var o in _objects)
        {
            if(o is EnemyObject)
                continue;
            if (o.transform.position.Equals(newPos))
                return false;
        }

        return newPos.x >= leftX && newPos.x <= rightX && newPos.y >= downY && newPos.y <= upY;
    }

    public void SetCat(CatObject catObject)
    {
        cat = catObject;
        _objects.Add(catObject);
    }

    public void SetCatBed(CatBedObject catBedObject)
    {
        catBed = catBedObject;
        _objects.Add(catBedObject);
    }

    public void AddEnemy(EnemyObject enemyObject)
    {
        _enemies.Add(enemyObject);
        _objects.Add(enemyObject);
    }
    
    public void AddBox(BoxObject boxObject)
    {
        _boxes.Add(boxObject);
        _objects.Add(boxObject);
    }

    public int GetCatWeight()
    {
        return cat.Weight;
    }
    
    public int GetPlayerWeight()
    {
        return playerWeight;
    }

    public CatBedObject GetBed()
    {
        return catBed;
    }

    public bool CheckLose(Vector3 newPos)
    {
        if(Lost)
        {
            LoseLevel();
            return true;
        }
        foreach (var enemy in _enemies)
        {
            if (enemy.transform.position.Equals(newPos))
            {
                LoseLevel();
                return true;
            }
        }

        return false;
    }
}