/*using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{
    public static GalleryManager Instance { get; private set; }
    
    public Transform[] currPage;
    public Transform[] prevPage;
    public Transform[] nextPage;

    public Button prev;
    public Button next;

    public float speed;

    const int PER_PAGE = 4;
    int _page = 1;
    bool _movingUp = false;
    bool _movingDown = false;
    [SerializeField]
    float _t = 0;

    List<GalleryModel> _models = new List<GalleryModel>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        prev.interactable = false;
        next.interactable = false;
    }

    public void AddModel(GalleryModel model)
    {
        int index = _models.Count;
        int position = index % 4;
        int page = (index / 4) + 1;

        if (!_models.Contains(model))
        {
            _models.Add(model);
            Transform wrapper = model.transform.parent;

            if (page > _page)
            {
                wrapper.position = nextPage[position].position;
            }
            else
            {
                wrapper.position = currPage[position].position;

                model.Display();
            }

            if (_page < Mathf.Ceil(((float)_models.Count) / PER_PAGE)) next.interactable = true;
        }
    }

    public void PreviousPage()
    {
        if (_movingDown || _movingUp) return;

        for (int i = (_page - 2) * 4; i < Mathf.Min((_page - 1) * 4, _models.Count); i++)
        {
            _models[i].Display();
        }

        _movingDown = true;
        next.interactable = true;
        if (_page == 2) prev.interactable = false;

        
    }

    public void NextPage()
    {
        if (_movingDown || _movingUp) return;

        for (int i = _page * 4; i < Mathf.Min((_page + 1) * 4, _models.Count); i++)
        {
            _models[i].Display();
        }

        _movingUp = true;
        prev.interactable = true;
        if (_page >= Mathf.Ceil(((float)_models.Count) / PER_PAGE) - 1) next.interactable = false;
    }

    private void Update()
    {
        if (!_movingUp && !_movingDown) return;

        _t += Time.deltaTime * speed;

        if (_t >= 1)
        {
            int currIndex = (_page - 1) * 4;
            int prevIndex = (_page - 2) * 4;
            int nextIndex = _page * 4;

            for (int i = 0; i < 4; i++)
            {
                if (_movingUp)
                {
                    
                    _models[currIndex + i].transform.parent.transform.position = prevPage[i].position;
                    _models[currIndex + i].Hide();
                    if (nextIndex + i < _models.Count)
                    {
                        _models[nextIndex + i].transform.parent.transform.position = currPage[i].position;
                    }
                }
                else if (_movingDown)
                {
                    _models[prevIndex + i].transform.parent.transform.position = currPage[i].position;
                    if (currIndex + i < _models.Count)
                    {
                        _models[currIndex + i].transform.parent.transform.position = nextPage[i].position;
                        _models[currIndex + i].Hide();
                    }
                }
            }

            if (_movingUp) _page++;
            else if (_movingDown) _page--;

            _movingUp = false;
            _movingDown = false;

            _t = 0;
        }
        else
        {
            float t = (-Mathf.Cos(_t * Mathf.PI) + 1) / 2;

            int currIndex = (_page - 1) * 4;
            int prevIndex = (_page - 2) * 4;
            int nextIndex = _page * 4;

            for (int i = 0; i < 4; i++)
            {
                if (_movingUp)
                {
                    _models[currIndex + i].transform.parent.transform.position = Vector3.Lerp(currPage[i].position, prevPage[i].position, t);
                    if (nextIndex + i < _models.Count)
                    {
                        _models[nextIndex + i].transform.parent.transform.position = Vector3.Lerp(nextPage[i].position, currPage[i].position, t);
                    }
                }
                else if (_movingDown)
                {
                    _models[prevIndex + i].transform.parent.transform.position = Vector3.Lerp(prevPage[i].position, currPage[i].position, t);
                    if (currIndex + i < _models.Count)
                    {
                        _models[currIndex + i].transform.parent.transform.position = Vector3.Lerp(currPage[i].position, nextPage[i].position, t);
                    }
                }
            }
        }
    }
}
*/