# Informe Trabajo Final 
# Optativo de Gráfico por Computadora 

### Integrantes
 - Daniel Orlando Ortiz Pacheco C-412
 - Javier Alejandro Valdes Gonzales C-411

## Introducción 
El proyecto consiste en la reprodución de una imagen inicial seleccionada por los estudiantes/integrantes.
Que guíados por las distintas implementaciones aportadas por el profesor deben lograr superar las fases de 
modelación y visualización, generando como producton final una nueva imagen lo más parecida posible a la 
selelcción inicial. Las implementaciones antes señaladas, han sido aportes paulatinos y antecedidos de 
conferencias con todos los detalles teóricos que se encuentran detrás de cada técnica y fase

## Escena 
    imagen work.vs.reality/objetive.jpg

En la misma se encuentran dos vasos de cristal llenos de agua, colocados en diagonal, fondo y piso de madera. 
Además de verse una toalla balnca y en la esquina superior derecha la boca de una jarra de cristal con agua, 
que verte la misma sobre el vaso más a la derecha. La selección se realizó basada en el reto que podría suponer 
una escena tan traslucida y en lo interesante que podría resultar la modelación de un efecto un tanto aleatorio 
y natural como puede ser la caida del agua. Un detalle en el que los estudiantes/integrantes se percataron y 
enfatizaron fue en el contorno de los vasos; en la imagen se observa que el mismo no es una línea, sino que 
presenta dos curvas de distinta pronunciación y un pequeño punto de inflexión entre ambas cerca del punto medio
de dicho contorno            

## Nube de Puntos 
    imagen work.vs.reality/cloud_pointers.jpg

En esta fase se logro representar los vasos, la jarra y la toalla. El resto resulto un tanto complicado, dado que 
fue la primera fase de la asignatura y aun se estaba ganando en habilidad. A pesar de eso la implementación de la 
nube de puntos ayudo a la sincronización de los previos conocimientos de geometría y otras ramas de la matemática
en función de la modelación de los distintos cuerpos. Para lograr la escena fue muy importante la clase Transforms
aportada por el profesor
 - La Toalla: No es más que un semiplano unitario 3D en su forma paramétrica.
 - Los Vasos: Para los vasos se unió un cilindro y un elipsoide, en sus respectivas formas paramétricas, ambos 
 cuerpos acotados por los planos seleccionados para lograr el contorno de la curva superior que es más pronunciada 
 que la inferior 
 - La Jarra:  Para el contorno de la jarra se usaron dos hiperboloides de una hoja, igualmente acotados por dos 
 planos 
El resto de la implemntación de la nube de puntos, pasa por la generación aleatoria de puntos con las distintas 
ecuaciones paramétricas y la colocación de cada cuerpo en particular en tamaño y posición correcta, madiante los 
métodos de la clase Transforms para asemejarse a la imagen obajetivo 

## Mallas
    imagen work.vs.reality/mesh.jpg

Tras el aporte de los métodos Surfase, Revolution, Generative y Lofted por el profesor, y la familiarización de los 
estudiantes/integrantes con las curvas bezier, se logra un nivel superior de detalle en la modelación de la escena
 - Los Vasos: El contorno curvo antes comentado se logró con una curva bezier y con su revolución se lográ el cuerpo.
 Dicha curva parte en el punto centro de la circunferencia de la base sube hasta la boca del vaso modelando el contorno
 y baja por el interior de igual manera, finalizando en el mismo punto de partida un poco más arriba. Opteniendo en el 
 cuerpo final el grosor del vidrio y el pequeño disco en la parte inferior del vaso 
 - La Jarra y Agua en los Vasos: Análogo a los vasos, pero con el contorno correcto 
 - Agua en la jarra: Debido a la posición de la jarra en la escena notesé que vasta con un trapecio que se generé con
 la curvatura de la caida del agua. Para la modelación de dicho trapecio se analizó las implemntaciones de los métodos
 Gererative y Lofted, concluyendo en una función que describe el lofted de dos rectas que se cortan y que el cuerpo 
 resultante es generado por una curva bezier. Dicho método termina siendo el parametro del método Surface
 - Agua en caída: Para la modelación de este efecto se partío de una curva bezier de 4 puntos donde el primerp y el ultimo 
 son iguales, algo parecido a un circulo aunque un poco deforme. Tomando dicha base se generó con otra bezier para darle 
 contorno y a la misma vez se agrego un pequeño delta aleatorio en la componente de profundidad de cada punto generado

## Raytricing, Pathtricing
    imagen work.vs.reality/ray.jpg
    imagen work.vs.reality/patht.jpg

En estas últimas fases del proyecto y la asigantura se le realizaron pocos cambios o agregaciones a los ejemplos e 
implementaciones aportadas. Guíados por los ejemplos se agregaron las mallas a la escena, se configuraron los distintos
materiales en su gran mayoría Fresnel y con índice de refraxión según el material. Para acercarse un poco más al objetivo
se agregaron más elementos a la escena 
 - Piso y paredes: Implmentados con los planos infinitos y con una imagen como textura 
 - Burbujas: Implmentadas con las esferas unitarias, y generadas de manera aleatoria en el interior de los vasos 
 - Agua en los Vasos: Tras la sugerencia del profesor; cuando un rayo de luz pasa de un material de mayor índice refraxión 
 a uno de menor se puede asumir que el rayo pasa directamente a aire, quedando por manejar solo cuando el rayo pasa 
 explicitamente de aire a el material de menor índice refraxión. Con lo cual el agua de los vasos fue modelada con un circulo 
 colocado en el interior del vaso marcando el inicio de dicho material. En particular al circulo del vaso de la derecha 
 se le imprimió el ruido anteriormente comentado en la componente de la altura, para simular como la caída del agua rompe 
 la planicidad de la superficie 