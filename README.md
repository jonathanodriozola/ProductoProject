# ProductoProject
#Trate de hacer lo que entendi, lo unico que no pude llegar a hacer es lo de que me traiga un producto de cada categoria,
#Lo que realice fue que me traiga todos los productos que puedan llegar a alcanzar y que seleccione el que quiera vender
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

