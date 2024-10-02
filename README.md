# TP Final de C# Nivel 2.
Gestión de Artículos para Catálogo de Comercio

Introducción

Presento una aplicación de escritorio diseñada para la gestión de artículos en un catálogo comercial. Esta solución es genérica, lo que la hace aplicable a cualquier tipo de comercio.

Funcionalidades Implementadas

La aplicación incluye las siguientes funcionalidades:

    Listado de Artículos: Visualiza todos los artículos disponibles en el catálogo.
    Búsqueda de Artículos: Permite buscar artículos mediante varios criterios, como código, nombre, marca y categoría.
    Agregar Artículos: Facilita la incorporación de nuevos artículos al catálogo.
    Modificar Artículos: Permite editar la información de artículos existentes.
    Eliminar Artículos: Opción para remover artículos del catálogo.
    Ver Detalle de un Artículo: Muestra información detallada de un artículo específico.

Estructura de Datos

Cada artículo en el catálogo incluye la siguiente información mínima:

    Código de Artículo
    Nombre
    Descripción
    Marca (seleccionable de una lista desplegable)
    Categoría (seleccionable de una lista desplegable)
    Imagen
    Precio

Desarrollo del Proyecto
Etapa 1: Modelado y Navegación

    Se desarrollaron las clases necesarias para el modelo de la aplicación.
    Se diseñaron las ventanas de la interfaz de usuario y se estableció la navegación entre ellas.

Etapa 2: Interacción con la Base de Datos

    Se implementó la conexión con una base de datos existente.
    Se añadieron las validaciones necesarias para garantizar la integridad de los datos y la funcionalidad del sistema.

Consideraciones Técnicas

    Se ha seguido una arquitectura en capas para mejorar la organización y mantenibilidad del código.
    Se implementó manejo de excepciones y validaciones para asegurar la estabilidad de la aplicación.

    Lenguaje de programación: C#
    Framework de desarrollo de escritorio: .NET Framework
    Base de datos: SQL Server
