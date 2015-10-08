using UnityEngine;
using System.Collections;

public class XUIClickResponder : MonoBehaviour
{
    // ----

    protected NGUIEventListener.VoidDelegate onSubmit;
    protected NGUIEventListener.VoidDelegate onClick;
    protected NGUIEventListener.VoidDelegate onDoubleClick;
    protected NGUIEventListener.BoolDelegate onHover;
    protected NGUIEventListener.BoolDelegate onPress;
    protected NGUIEventListener.BoolDelegate onSelect;
    protected NGUIEventListener.FloatDelegate onScroll;
    protected NGUIEventListener.VectorDelegate onDrag;
    protected NGUIEventListener.ObjectDelegate onDrop;
    protected NGUIEventListener.StringDelegate onInput;

    protected NGUIEventListener.VoidDelegate onPreClick;

    [HideInInspector]
    public bool PreClickValid;

    public virtual void OnSubmit() { if (onSubmit != null) onSubmit(gameObject); }
    public virtual void OnClick() 
    {
        PreClickValid = true;
        if (onPreClick != null)
        {
            onPreClick(gameObject);
        }
        if (!PreClickValid)
        {
            return;
        }
        if (onClick != null) onClick(gameObject); 
    }
    public virtual void OnDoubleClick() { if (onDoubleClick != null) onDoubleClick(gameObject); }
    public virtual void OnHover(bool isOver) { if (onHover != null) onHover(gameObject, isOver); }
    public virtual void OnPress(bool isPressed) { if (onPress != null) onPress(gameObject, isPressed); }
    public virtual void OnSelect(bool selected) { if (onSelect != null) onSelect(gameObject, selected); }
    public virtual void OnScroll(float delta) { if (onScroll != null) onScroll(gameObject, delta); }
    public virtual void OnDrag(Vector2 delta) { if (onDrag != null) onDrag(gameObject, delta); }
    public virtual void OnDrop(GameObject go) { if (onDrop != null) onDrop(gameObject, go); }
    //public virtual void OnInput(string text) { if (onInput != null) onInput(gameObject, text); }
    public virtual void OnInputText(string text) { if (onInput != null) onInput(gameObject, text); }

    public virtual void AddPreResponder(NGUIEventListener.VoidDelegate del)
    {
        onPreClick += del;
    }

    public virtual void RemovePreResponder(NGUIEventListener.VoidDelegate del)
    {
        onPreClick -= del;
    }


    public virtual void AddSubmitDelegate(NGUIEventListener.VoidDelegate del)
    {
        onSubmit += del;
    }
    public virtual void RemoveSubmitDelegate(NGUIEventListener.VoidDelegate del)
    {
        onSubmit -= del;
    }

    /// <summary>
    /// Adds a method to be called when the value of a control changes (such as a checkbox changing from false to true, or a slider being moved).
    /// </summary>
    public virtual void AddClickDelegate(NGUIEventListener.VoidDelegate del)
    {
        onClick += del;
    }

    /// <summary>
    /// Removes a method added with AddClickDelegate().
    /// </summary>
    public virtual void RemoveClickDelegate(NGUIEventListener.VoidDelegate del)
    {
        onClick -= del;
    }


    public virtual void AddHoverDelegate(NGUIEventListener.BoolDelegate del)
    {
        onHover += del;
    }

    /// <summary>
    /// Removes a method added with AddHoverDelegate().
    /// </summary>
    public virtual void RemoveHoverDelegate(NGUIEventListener.BoolDelegate del)
    {
        onHover -= del;
    }


    public virtual void AddPressDelegate(NGUIEventListener.BoolDelegate del)
    {
        onPress += del;
    }

    /// <summary>
    /// Removes a method added with AddPressDelegate().
    /// </summary>
    public virtual void RemovePressDelegate(NGUIEventListener.BoolDelegate del)
    {
        onPress -= del;
    }
}
