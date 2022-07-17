using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqSnippets
{
    public class LinqSnippets
    {
        static public void BasicLinQ()
        {

            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León"
            };


            // 1. SELECT * of cars (SELECCIONA TODOS LOS COCHES)
            var carList = from car in cars select car;

            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }

            // 2. SELECT WHERE car is Audi (SELECCIONA LOS AUDI)
            var audiList = from car in cars where car.Contains("Audi") select car;

            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }

        }

        // Number Examples
        static public void LinqNumbers()
        {

            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Multiplica el número por 3
            // Coje todos los números menos el 9
            // Ordena los números ascendente

            var processedNumberList =
                numbers
                    .Select(num => num * 3) // { 3, 6, 9, etc...)
                    .Where(num => num != 9) // { Todos menos 9 }
                    .OrderBy(num => num); // Ordena ascendente
        }


        static public void SearchExamples()
        {

            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c"
            };

            // 1. Primero de todos los elementos
            var first = textList.First();

            // 2. Primer elemento que sea "c"
            var cText = textList.First(text => text.Equals("c"));

            // 3. Primer elemento que contenga "j"
            var jText = textList.First(text => text.Contains("j"));

            // 4. Primer elemento que contenga "z" o sino será por defecto
            var firstOrDefaultText = textList.FirstOrDefault(text => text.Contains("z")); // "" o el primer elemento que contenga "z"

            // 5. Último elemento que contenga "z" o sino será por defecto
            var lastOrDefaultText = textList.LastOrDefault(text => text.Contains("z")); // "" o el último elemento que contenga "z"

            // 6. Valor único
            var uniqueTexts = textList.Single();
            var uniqueorDefaultTexts = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            
            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers); // {4, 8}

        }

        static public void MultipleSelects()
        {

            // Seleccionar todo
            string[] myOpinions =
            {
                "Opinión 1, text 1",
                "Opinión 2, text 2",
                "Opinión 3, text 3"
            };

            var myOpinonSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new []
                    {
                        new Employee
                        {
                            Id=1,
                            Name="Martín",
                            Email="martin@imagingroup.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id=2,
                            Name="Pepe",
                            Email="pepe@imagingroup.com",
                            Salary = 1000
                        },
                        new Employee
                        {
                            Id=3,
                            Name="Juanjo",
                            Email="juanjo@imagingroup.com",
                            Salary = 1500
                        }
                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new []
                    {
                        new Employee
                        {
                            Id=4,
                            Name="Ana",
                            Email="ana@imagingroup.com",
                            Salary = 3500
                        },
                        new Employee
                        {
                            Id=5,
                            Name="Maria",
                            Email="maria@imagingroup.com",
                            Salary = 1500
                        },
                        new Employee
                        {
                            Id=6,
                            Name="Marta",
                            Email="marta@imagingroup.com",
                            Salary = 4000
                        }
                    }
                }
            };

            // Obtener todos los empleados de todas las empresas
            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            // Saber si cualquier lista está vacía
            bool hasEnterprises = enterprises.Any();

            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            // Todas las empresas con salarios de 1000€ o más
            bool hasEmployeeWithSalaryMoreThanOrEqual1000 =
                enterprises.Any(enterprise =>
                    enterprise.Employees.Any(employee => employee.Salary >= 1000));

        }

        static public void linqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "c", "d" };

            // INNER JOIN
            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(
                    secondList,
                    element => element,
                    secondElement => secondElement,
                    (element, secondElement) => new { element, secondElement }
                );


            // OUTER JOIN - LEFT
            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };

            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = secondElement };



            // OUTER JOIN - RIGHT
            var rightOuterJoin = from secondElement in secondList
                                 join element in firstList
                                 on secondElement equals element
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where secondElement != temporalElement
                                 select new { Element = secondElement };


            // UNION
            var unionList = leftOuterJoin.Union(rightOuterJoin);

        }


        static public void SkipTakeLinq()
        {

            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };

            // SKIP

            var skipTwoFirstValues = myList.Skip(2); // { 3,4,5,6,7,8,9,10 }

            var skipLastTwoValues = myList.SkipLast(2); // {1,2,3,4,5,6,7,8 }

            var skipWhileSmallerThan4 = myList.SkipWhile(num => num < 4); // { 4,5,6,7,8 }

            // TAKE

            var takeFirstTwoValues = myList.Take(2); // { 1,2 }

            var takeLastTwoValues = myList.TakeLast(2); // { 9,10 }

            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4); // { 1,2,3 }

        }


        // Paginado con Skip & Take
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            int startIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage);
        }


        // Variables
        static public void LinqVariables()
        {

            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquared = Math.Pow(number, 2)
                               where nSquared > average
                               select number;

            Console.WriteLine("Avegare: {0}", numbers.Average());

            foreach (int number in aboveAverage)
            {
                Console.WriteLine("Number: {0} Square: {1} ", number, Math.Pow(number, 2));
            }

        }

        // ZIP
        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "one", "two", "three", "four", "five" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + "=" + word);

            // { "1=one", "2=two", ... }
        }

        // Repeat & Range
        static public void repeatRangeLinq()
        {
            // Generar colección de 1 - 1000 --> RANGE
            IEnumerable<int> first1000 = Enumerable.Range(1, 1000);

            // Repetir valor N veces
            IEnumerable<string> fiveXs = Enumerable.Repeat("X", 5); // {"X","X","X","X","X"}

        }


        static public void studentsLinq()
        {

            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Martín",
                    Grade = 90,
                    Certified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "Juan",
                    Grade = 50,
                    Certified = false,
                },
                new Student
                {
                    Id = 3,
                    Name = "Ana",
                    Grade = 96,
                    Certified = true,
                },
                new Student
                {
                    Id = 4,
                    Name = "Álvaro",
                    Grade = 10,
                    Certified = false,
                },
                new Student
                {
                    Id = 5,
                    Name = "Pedro",
                    Grade = 50,
                    Certified = true,
                }
            };


            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;

            var notCertifiedStudents = from student in classRoom
                                       where student.Certified == false
                                       select student;

            var appovedStudentsNames = from student in classRoom
                                       where student.Grade >= 50 && student.Certified == true
                                       select student.Name;

        }

        // ALL
        static public void AllLinq()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };
            bool allAreSmallerThan10 = numbers.All(x => x < 10); // true
            bool allAreBiggerOrEqualThan2 = numbers.All(x => x >= 2); // false

            var emptyList = new List<int>();
            bool allNumbersAreGreaterThan0 = numbers.All(x => x >= 0); // true

        }

        // Aggregate
        static public void aggregateQueries()
        {

            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Sum all numbers
            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);

            // 0, 1 => 1
            // 1, 2 => 3
            // 3, 4 => 7
            // etc.


            string[] words = { "hello,", "my", "name", "is", "Martín" }; // hello, my name is Martín
            string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + current);

            // "", "hello," => hello,
            // "hello,", "my" => hello, my
            // "hello, my", "name" => hello, my name
            // etc.
        }

        // Disctinct
        static public void distinctValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
            IEnumerable<int> distinctValues = numbers.Distinct();
        }

        // GroupBy
        static public void groupByExamples()
        {

            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Obtener los números pares y generar dos grupos
            var grouped = numbers.GroupBy(x => x % 2 == 0);

            // Tendremos dos grupos:
            // 1. El que no cumple la condición (Impares)
            // 2. El que cumple la condición (Pares)

            foreach (var group in grouped)
            {
                foreach (var value in group)
                {
                    Console.WriteLine(value); // 1,3,5,7,9 ... 2, 4, 6, 8 (first the odds and then the even)
                }
            }

            // Another Example
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Martín",
                    Grade = 90,
                    Certified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "Juan",
                    Grade = 50,
                    Certified = false,
                },
                new Student
                {
                    Id = 3,
                    Name = "Ana",
                    Grade = 96,
                    Certified = true,
                },
                new Student
                {
                    Id = 4,
                    Name = "Álvaro",
                    Grade = 10,
                    Certified = false,
                },
                new Student
                {
                    Id = 5,
                    Name = "Pedro",
                    Grade = 50,
                    Certified = true,
                }
            };


            var certifiedQuery = classRoom.GroupBy(student => student.Certified);

            // We obtain two groups
            // 1- Not certified students
            // 2- Certified Students

            foreach (var group in certifiedQuery)
            {
                Console.WriteLine("--------- {0} --------", group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine(student.Name);
                }
            }
        }


        static public void relationsLinq()
        {

            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id= 1,
                    Title = "My first post",
                    Content = "My first content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 1,
                            Created = DateTime.Now,
                            Title = "My first comment",
                            Content = "My content"
                        },
                        new Comment()
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title = "My second comment",
                            Content = "My other content"
                        }
                    }
                },
                new Post()
                {
                    Id= 2,
                    Title = "My second post",
                    Content = "My second content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 3,
                            Created = DateTime.Now,
                            Title = "My other comment",
                            Content = "My new content"
                        },
                        new Comment()
                        {
                            Id = 4,
                            Created = DateTime.Now,
                            Title = "My other new comment",
                            Content = "My new content"
                        }
                    }
                }
            };


            var commentsContent = posts.SelectMany(
                    post => post.Comments,
                        (post, comment) => new { PostId = post.Id, CommentContent = comment.Content });

        }


    }
}