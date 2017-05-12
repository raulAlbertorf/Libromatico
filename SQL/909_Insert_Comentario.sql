INSERT INTO 
		comentario(Id,Texto,FechaCreacion,Prestamo_Id, Perfil_Id) 
	VALUES
		(1, "Texto 1", '2016-11-01 18:37:14', 1, 1),
		(2, "Texto 2", now(), 1, 2),
		(3, "Texto 3", '2016-11-03 18:37:14', 2, 3),
		(4, "Texto 4", now(), 2, 2),
		(5, "Texto 5", now(), 1, 2),
		(6, "Texto 6", '2016-11-08 18:37:14', 3, 3),
		(7, "Texto 7", now(), 3, 4),
		(8, "Texto 8", '2016-10-08 18:37:14', 1, 2),
		(9, "Texto 9", now(), 5, 1),
		(10, "Texto 10", '2016-11-02 18:37:14', 5, 6);