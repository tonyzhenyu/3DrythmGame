public interface IDataParseType<T> where T:new()
{
    void ParseData(T nodeStruct, string data);
}

