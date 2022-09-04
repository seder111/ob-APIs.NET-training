# 7. Extendiendo servicios, protección de rutas mediante RBAC y probando JWT en Swagger - APIs con .NET 

## Ejercicio:

* Replica las configuraciones para trabajar con JWT

    * Asegúrate de entender el archivo que extiende los servicios del builder

    * Asegúrate de entender cómo se protegen las rutas

    * Asegúrate de entender cómo pasar el Bearer JWT Token por Swagger

* Modifica el controller de Accounts para que:

    * Usando Linq, busque en la lista de usuarios del contexto de la base de datos
 
    * Verifique tanto el nombre como la contraseña del usuario
 
    * Obtenga la primera coincidencia

* Actualiza el modelo de usuarios para segurar que tengan Roles

    * Administrador

    * Usuario

* Gestiona las rutas de tu aplicación para que solo los administradores puedan:

    * Realizar operaciones de Modificación, Eliminación o Creación en tu proyecto

* Agregado que un administrador pueda modificar el rol de un usuario.



---
By _Sergio González_