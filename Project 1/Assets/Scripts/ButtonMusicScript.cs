using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMusicScript : MonoBehaviour
{
    public AudioSource Undo, Redo, Sell, F1, F2, F3;

   public void UndoSound() { Undo.Play(); }

   public void RedoSound() { Redo.Play(); }

   public void SellSound() { Sell.Play(); }

    public void FinishedSound()
    {
        F1.Play();
        F2.Play();
        F3.Play();
    }

}
