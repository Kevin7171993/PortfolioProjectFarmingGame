using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StandardFunctionLib
{
    public static void Swap<T>(ref T val_1, ref T val_2)
    {
        T buf = val_1;
        val_1 = val_2;
        val_2 = buf;
    }
}