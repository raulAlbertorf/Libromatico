INSERT INTO 
		prestamo 
			(Id,Estado,FechaEnvio,FechaRecepcion,FechaExpiracion,
			CalificacionAlReceptor,CalificacionAlPrestamista,
			Item_Id,Prestamista_Id,Receptor_Id)
	VALUES 
		(1, 0, now(), now(), now(), 1, 2, 1, 1, 2),
		(2, 1, now(), now(), now(), 3, 4, 2, 2, 3),
		(3, 2, now(), now(), now(), 5, 6, 3, 3, 4),
		(4, 3, now(), now(), now(), 4, 5, 4, 4, 5),
		(5, 4, now(), now(), now(), 2, 2, 5, 1, 6),
		(6, 0, now(), now(), now(), 1, 2, 6, 5, 1),
		(7, 0, now(), now(), now(), 3, 4, 7, 4, 2),
		(8, 1, now(), now(), now(), 4, 3, 8, 6, 3),
		(9, 2, now(), now(), now(), 5, 5, 8, 6, 4),
		(10, 3, now(), now(), now(), 4, 4, 7, 4, 5),
		(11, 4, now(), now(), now(), 5, 5, 6, 5, 3),
		(12, 4, now(), now(), now(), 4, 5, 2, 2, 4),
		(13, 3, now(), now(), now(), 1, 2, 3, 3, 5);