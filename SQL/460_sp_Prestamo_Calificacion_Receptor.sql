--DELIMITER $$
DROP PROCEDURE IF EXISTS sp_Prestamo_Calificacion_Receptor $$

CREATE PROCEDURE sp_Prestamo_Calificacion_Receptor(
	inPrestamo_Id BIGINT,
	inCalificacion BIGINT
)
BEGIN
	UPDATE Prestamo
	SET 
		CalificacionAlReceptor = inCalificacion
	WHERE
		Id = inPrestamo_Id;
END
$$