
# IngeCrush

Este proyecto consiste en un juego tipo puzzle con tematica de Ingemones; unos caracteristicos personajes de Ingenia, de la facultad de ingenieria de la universidad de Antioquia. Desarrollado por estudiantes del semillero de  videojuegos de la universidad como experiencia para el stand de Ingenia en expoingeniería del año 2023.


## Jugabilidad

El proyecto consiste en un juego sencillo tipo puzzle (Vea juegos similares como candy crush o bejeweld), con un total de 5 niveles, el jugador deberá combinar lineas de 3 o más ingemones del mismo tipo hasta realizar el puntaje necesario para completar el nivel.






## Construcción

El proyecto consiste de 5 scripts encargados del funcionamiento

- Tile: abstracción de tipo entidad para una casilla del juego

- Puntaje: controlador encargado de manejar  el Puntaje

- Menu:  controlador  del menu principal

- MenuNiveles:  controlador de los 5 niveles principales

- Match3GameManager: controlador principal del juego, se encarga del movimiento de las fichas y de las comprobaciones correspondientes cada que una ficha cambia de lugar, tambien se encarga de generar el tablero inicial de cada niveles

## Build

Para correr el proyecto deberá descargar o clonar el repositorio, luego deberá abrirlo mediante unity hub utilizando la versión 
2022.3.4f1 o cualquier versión compatible, luego podrá crear un build y hacer un despliegue de la aplicación usando las Build Settings de unity

