PROYECTO FINAL: Francisco Miguel Gimenez

Detalles del juego:

El juego consiste en un rpg de combate por turos que tiene dos escenarios distintos. El primero, es un mundo top-down en el cual se tiene libertad para caminar. En el mundo habrán enemigos patrullando a los cuales te deberás enfrentar en otro escenario de combate por turnos si te los encuentras. Una vez derrotes a los enemigos (o no) podrás cruzar una puerta que finalizará el nivel.


Lo desarrollé usando Godot Engine 4.x, en su versión "GodotSharp" que me permite utilizar C# para hacer el código de mi juego, en lugar de GDscript que es el lenguaje creado por Godot.
La decisión de hacerlo con Godot vino de que antes programé con Unity, entonces tenía algo de idea sobre lo que es el desarrollo de videojuegos, y también algo de C#. Solo tuve que leer la documentación (https://docs.godotengine.org/es/4.x/index.html) para entender los conceptos básicos generales, cómo afecta eso a C#. También tuve que leer un par de foros para terminar de comprender cómo se implementaban ciertas cosas que están explicadas para GDscript y no muy bien para C# como son las señales.
Por otra parte porque no quería hacer cosas ASCII, jaja.

También decidí usar C# porque este me dejaba trabajar de una forma más ordenada, y casi me obligaba. Tener namespaces y archivos específicos, también un poco más legible, conociendo cómo trabajo con Python, que es el lenguaje en el que se basó GDscript.

RECURSOS:

API: "http://api.openweathermap.org/data/2.5/weather"
Formas de uso del API: https://openweathermap.org/current

La API que implementé en mi juego detecta la provincia en la que esté configurado el juego y busca el clima actual de esa zona. Esto detectará si llueve o no. En caso de estar despejado, el juego se verá claro. De otra forma, el mapa se verá más oscuro y caerá lluvia en él. También, cuando el juego esté en pausa se verán los datos de la partida. Entre estos, figurará el clima actual.