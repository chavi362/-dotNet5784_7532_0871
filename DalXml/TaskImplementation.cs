

namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;


internal class TaskImplementation : ITask
{
    public Task? Read(Func<Task, bool> filter)
    {
        XElement? tasksArray = XMLTools.LoadListFromXMLElement("tasks");
        int id = Config.NextTaskId;
        Task copy = t with { Id = id };//puting the Running ID number
        DataSource.Tasks.Add(copy);//add the task to the data
        return id;
    }

    public int Create(Task t)
    {
        XElement? tasksArray = XMLTools.LoadListFromXMLElement("tasks");
        int id = Config.NextTaskId;
        Task copy = t with { Id = id };//puting the Running ID number
        tasksArray.Add(copy);//add the task to the data
        return id;
        int id = Config.NextTaskId;
       // XElement? Config = XDocument.Load("../config.xml").Root;
        XElement? idOrder = Config?.Element("OrderId");
        int id = Convert.ToInt32(idOrder?.Value);
        value.Id = id++;
        idOrder.Value = id.ToString();
        Config?.Save("../config.xml");
        Task copy = t with { Id = id };//puting the Running ID number
        DataSource.Tasks.Add(copy);//add the task to the data
        return id;
        //if (value.Id == 0)//status of add
        //{
        //    XElement? Config = XDocument.Load("../config.xml").Root;
        //    XElement? idOrder = Config?.Element("OrderId");
        //    int id = Convert.ToInt32(idOrder?.Value);
        //    value.Id = id++;
        //    idOrder.Value = id.ToString();
        //    Config?.Save("../config.xml");
        //}
        //List<DO.Order> lst = GetAll().ToList();
        //lst.Add(value);
        //StreamWriter write = new StreamWriter("../Order.xml");
        //XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>));
        //ser.Serialize(write, lst);
        //write.Close();
        //return value.Id;
    }

    void ICrud<Task>.Delete(int id)
    {
        XElement? Tasks = XDocument.Load("../tasks.xml").Root;
        Tasks?.Elements().ToList().Find(task => Convert.ToInt32(task?.Element("Id")?.Value) == id)?.Remove();
        Tasks?.Save("../Product.xml");
    }

    Task? ICrud<Task>.Read(int id)
    {
        throw new NotImplementedException();
    }

    IEnumerable<Task?> ICrud<Task>.ReadAll(Func<Task, bool>? filter)
    {
        throw new NotImplementedException();
    }

    void ICrud<Task>.Update(Task item)
    {
        throw new NotImplementedException();
    }
}
