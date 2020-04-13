Examen tecnico para Acciona
Se publican 2 endpoints, ejemplo de ejecucion en host 'localhost' y puerto 2556:

	LoginController:	http://localhost:2556/api/login/Login
	ProvinciaController:	http://localhost:2556/api/provincia/Obtener?nombre=tierra

Repositorio Git: https://github.com/carlosaqr/ExamenTecnicoAcciona.git

Consideraciones:

	Login: 
		El endpoint Login usa solamente el protocolo POST.
		Se deben pasar los parametros UserName y Password para loggear el usuario.
		En el web.config se definen la combinacion de valores validos
		Ejemplo de parametros en formato JSON:
		{
		UserName: 'admin',
		Password: 'admin'
		}

	Provincia: 
		Se configuró solamente para ejecutar por GET, pero se parametrizó el metodo 
		para forzar el lanzamiento de una excepcion de protocolo en tiempo de ejecucion
		La variable "nombre" no requiere el nombre completo de la provincia

	Logging:
		El archivo de log se encuentra en "logs/MyLogFile.txt"
		la configuracion se encuentra en el web.config

	Frameworks:
		Se utilizaron los siguientes frameworks para el desarrollo:
			Unity Framework
			Log4Net

	UnitTests:
		Por falta de tiempo no pude crear mas casos de prueba

