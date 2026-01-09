using System;
using BerryAIGen.Common;

namespace BerryAIGen.Toolkit.Thumbnails;

public class Job<T, TOut>
{
    public T Data { get; set; }
    public Action<TOut> Completion { get; set; }
}







