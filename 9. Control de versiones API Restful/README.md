# 9. Control de versiones de una API Restful - APIs con .NET 

## Ejercicio:

* Replica lo visto en la sesión para crear dos versiones distintas de una misma API

    * Asegúrate de entender qué instalaciones son necesarias

    * Asegúrate de entender cómo documentar la versión en controladores y rutas

    * Asegúrate de entender el archivo de configuración y personalizaciónd e información de Swagger

    * Asegúrate de entender todas las configuraciones realizadas en Program.cs

* Crea un nuevo proyecto desde cero de tipo API Restful

    * Utilizando la URL https://fakestoreapi.com/products

        * Plantea un DTO de Producto y otro de Rating para la versión 1 de tu API restful

            * Producto:

                * id (int)

                * title (string)

                * price (float)

                * description (string)

                * category (string)

                * image (string)

                * rating (Rating)

            * Rating:

                * rate (float)

                * count (int)

        * Plantea un DTO de Producto y otro de Rating para la versión 2 de tu API restful

            * Producto:

                * InternalId (Guid)

                * int id

                * title (string)

                * price (float)

                * description (string)

                * category (string)

               * image (string)

        * Crea dos controladores, uno para cada versión

            * Versión 1:

                * Que devuelva la lista de productos con sus respectivo rating

            * Versión 2:

                * Que devuelva la lsita de productos con un InternalID de tipo Guid nuevo para cada uno

                    * Para ello, puedes usar Guid.NewGuid()

        * Asegúrate de documentar correctamente tu API y de que Swagger muestre ambas versiones para ser probadas

---
By _Sergio González_