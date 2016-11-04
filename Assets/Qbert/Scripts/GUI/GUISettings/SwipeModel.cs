using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Qbert.Scripts;
using Assets.Qbert.Scripts.GameScene;
using Assets.Qbert.Scripts.GameScene.Characters;
using Assets.Qbert.Scripts.Utils;

public class SwipeModel<T> : MonoBehaviour, IEnumerable<T> where T : MonoBehaviour
{
    public T currentModel;
    public Transform rootModels;

    public float scaleModelNoFocus = 0.8f;
    public float modelsOffset;

    public GameObject swipeHelpShow;
    public List<T> models;

    private Coroutine scrollInWork;

    protected void ShowHelpHand(bool isShow)
    {
        swipeHelpShow.gameObject.SetActive(isShow);
    }

    protected virtual void CreateModels(T[] prefabs)
    {
        var characters = prefabs;
        models = new List<T>(characters.Length);

        for (int i = 0; i < characters.Length; i++)
        {
            var createCharacter = Instantiate(characters[i]);
            models.Add(createCharacter);

            var tr = createCharacter.transform;
            tr.SetParent(rootModels);
            tr.localRotation = Quaternion.Euler(0, 0, 0);
            tr.localPosition = new Vector3(i * modelsOffset, 0, 0);
            tr.localScale = new Vector3(scaleModelNoFocus, scaleModelNoFocus, scaleModelNoFocus);

        }

        currentModel = models[0];
        currentModel.transform.localScale = new Vector3(
            1, 1, 1);
    }

    public virtual void Scroll(bool isLeft)
    {
        int index = models.IndexOf(currentModel);

        if (isLeft && index >= models.Count - 1)
        {
            return;
        }

        if (!isLeft && index <= 0)
        {
            return;
        }

        var first = currentModel;
        var second = isLeft ? models[index + 1] : models[index - 1];
        ScrollToModel(first, second, 0.3f);
        currentModel = second;

        ShowHelpHand(false);
        GlobalValues.isShowHelpHandToSelectCharacter = false;
        GlobalValues.Save();
    }

    public void SetFocusToModel(T model)
    {
        ScrollToModel(currentModel, model, 0.0f);
        currentModel = model;
    }

    public IEnumerator MovingLocalTransformTo(Transform tr, Vector3 movingTo, float time)
    {
        float distance = Vector3.Distance(tr.localPosition, movingTo);
        float speedMoving = distance / time;

        while (distance > 0.001f)
        {
            Vector3 move = Vector3.MoveTowards(tr.localPosition, movingTo, speedMoving * Time.deltaTime);
            tr.localPosition = move;
            distance = Vector3.Distance(tr.localPosition, movingTo);
            yield return null;
        }

        scrollInWork = null;
    }


    public void ScrollToModel(T first, T second, float speedMove)
    {
        float x = first.transform.localPosition.x - second.transform.localPosition.x;
        x *= 0.3f;
        var moveTo = rootModels.localPosition + new Vector3(0, 0, x);
        scrollInWork = StartCoroutine(MovingLocalTransformTo(rootModels, moveTo, speedMove));
        StartCoroutine(this.ScaleTranformTo(first.transform, scaleModelNoFocus, speedMove));
        StartCoroutine(this.ScaleTranformTo(second.transform, 1.0f, speedMove));
    }

    public void OnScrollEvent(DirectionMove.Direction dir)
    {
        if (scrollInWork == null)
        {
            if (dir == DirectionMove.Direction.DownLeft)
            {
                Scroll(true);
            }
            else if (dir == DirectionMove.Direction.UpRight)
            {
                Scroll(false);
            }
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return models.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
