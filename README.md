# ProductoProject
#Pude realizar todo el proyecto que me hicieron realizar, la unica parte en la que logre desarrollar pero de una manera distinta fue la siguiente:
--*Se deberá ofrecer el de mayor precio.*
#Lo que hice aca traje los productos que al cliente le alcance con el presupuesto dado de ambas categorias, por ejemplo si el pone que su presupuesto es 300
y en la tabla estan los siguientes productos:

id  	precio  fecha_carga 	categoria_id 
1	200.00	2023-05-25	1
2	500.00	2023-05-25	1
3	300.00	2023-05-25	1

4	100.00	2023-05-25	2
5	400.00	2023-05-17	2
6	700.00	2023-05-05	2
7	500.00	2023-05-26	2
8	300.00	2023-05-26	2

Categorias

id  	nombre
1	PRODUNO
2	PRODDOS


Suponiendo si el presupuesto es 300 trae lo siguiente:
le va a traer esto:
1	200.00	2023-05-25	PRODUNO
3	300.00	2023-05-25	PRODUNO
4	100.00	2023-05-25	PRODDOS
8	300.00	2023-05-26	PRODDOS


#La conexion a la base de datos obviamente esta en web.config, lo realice en sql server managment, adjunto codigo de las tablas que utilice:
CREATE TABLE Categorias (
    id INT PRIMARY KEY,
    nombre VARCHAR(50)
);

CREATE TABLE Stock (
	id INT IDENTITY(1,1) PRIMARY KEY,
    precio DECIMAL(10, 2),
    fecha_carga DATE,
    categoria_id INT,
    FOREIGN KEY (categoria_id) REFERENCES Categorias(id)
);

#Lo que hace el programa basicamente es en la pantalla principal, stock vos podes agregar productos de la categoria que seleccione y se adjunta en la tabla alojada mas abajo.
#luego vas a tener el poder de editar o eliminar el producto que quieras.
#en la parte de filtro bueno ya lo mencionado, que el presupuesto que vos coloques vas a poder seleccionar el que quieras vender.

#adjunto tambien el procedimiento de cada funcion hechas



#"AgregarProducto(StockModel producto)": Esta funcion se llama cuando se envía un formulario para agregar un nuevo producto. Inserta el nuevo producto en la base de datos a través del "ControlStockContext" y va de nuevo a la pagina de inicio, la actualiza.
#"Filtrar(decimal presupuesto)": Esta funcion se llama cuando se envía el formulario de filtrado con un presupuesto específico. Realiza un filtrado de los productos disponibles en función del presupuesto utilizando un algoritmo de programación dinámica y devuelve los productos filtrados en la vista "FiltrarResultados.cshtml" junto con la diferencia entre el presupuesto y el precio total de los productos filtrados.
#"Eliminar(int id)": Esta funcion Elimina el producto de la base de datos a través de contexto "ControlStockContext" y redirige a la pagina de inicio, el proceso de eliminacion lo hace por id
#"Editar(int id)": Esta funcion edita el elemento seleccionado de la tabla, lo edito al travez de su id y lo actaulaiza al traves del contexto "ControlStockContext"
#"GuardarEdicion(StockModel producto)": Esta funcion se llama cuando se envía el formulario de edición de un producto. Actualiza los datos del producto en la base de datos a través del contexto "ControlStockContext" y redirige a la pagina de inicio
#"Vender(int[] productosSeleccionados)": Esta acción se llama cuando se realiza una venta de productos seleccionados. Elimina los productos seleccionados de la base de datos a través del contexto "ControlStockContext".



#Dentro de "/ProductoProject/Data/ControlStockContext.cs"
#van a tener que cambiar la llamada a la base de datos que en este caso yo lo tengo asi en web.config:

  <connectionStrings>
    <add name="ControlStockConnection" connectionString="Data Source=JONATHAN-DEV;Integrated Security=true;Initial Catalog=ControlStock" providerName="System.Data.SqlClient" />
  </connectionStrings>
  
#y dendtro de "ControlStockContext.cs"
#hago la llamada de la siguiente manera:
connectionString = ConfigurationManager.ConnectionStrings["ControlStockConnection"].ConnectionString;

#si quieren pueden cambiarle el nombre a la conexion y tendran que hacer la conexion a su base de datos.


