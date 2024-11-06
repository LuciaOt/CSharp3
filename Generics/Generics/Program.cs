IEnumerable<string> strings = ["test2", "test3", "test4"];
IEnumerable<int> integers = [2, 3, 4];
var stringoveInt = integers.Select(i => i.ToString());
//Console.Write(Max("test", "5"));
Console.Write(MaxCollection([1, 2, 3, 4, 5]));

//Max<int>(3,5)

void WriteToConsole<T>(IEnumerable<T> list) where T : IComparable<T>
{
    foreach (var s in list)
    {
        Console.Write(s.ToString() + " ");
    }

}

T Max<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b) > 0 ? a : b;
}

T MaxCollection<T>(IEnumerable<T> values) where T : IComparable<T>
{
    return values.Max();
    // T max = values.FirstOrDefault();
    // if (max = null)
    // {
    //     throw new Exception();
    // }
    // return.values.Max();
    // foreach (var value in values)
    // {
    //     if (value.CompareTo(max) > 0)
    //     {
    //         max = value;
    //     }

    // }


}

bool IsDistinct<T>(IEnumerable<T> values) where T : IComparable<T>
{
    HashSet<T> seenvalues = new HashSet<T>();
    foreach (var value in values)
    {
        if (seenvalues.Contains(value))
        {
            return false;

        }
        seenvalues.Add(value);
    }
    return true;
}


