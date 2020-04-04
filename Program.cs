using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFluentNhibernate
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var session = NhibernateHlper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var fordMake = new Make { name = "Ford" };
                    var fordModel = new Model { name = "fordModel", Make = fordMake };
                    var Car = new Car { title = "BMW", Make = fordMake, Model = fordModel };
                    session.Save(Car);
                    transaction.Commit();
                    Console.WriteLine();
                }
            }
        }
    }
    public class Make
    {
        public virtual int id { get; set; }
        public virtual string name { get; set; }
    }
    public class Model
    {
        public virtual int id { get; set; }
        public virtual string name { get; set; }
        public virtual Make Make { get; set; }
    }
    public class Car
    {
        public virtual int id { get; set; }
        public virtual string title { get; set; }
        public virtual Make Make { get; set; }
        public virtual Model Model { get; set; }

    }
    public class MakeMap : ClassMap<Make>
    {
        public MakeMap()
        {
            Id(x => x.id);
            Map(x => x.name);
        }
    }
    public class ModelMap : ClassMap<Model>
    {
        public ModelMap()
        {
            Id(x => x.id);
            Map(x => x.name);
            References(x => x.Make).Cascade.All();
        }
    }

    public class CarMap : ClassMap<Car>
    {
        public CarMap()
        {
            Id(x => x.id);
            Map(x => x.title);
            References(x => x.Make).Cascade.All(); ;
            References(x => x.Model).Cascade.All(); ;

        }
    }
}
