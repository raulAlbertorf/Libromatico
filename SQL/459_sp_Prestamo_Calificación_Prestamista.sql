--DELIMITER $$
DROP PROCEDURE IF EXISTS sp_Prestamo_Calificacion_Prestamista $$

CREATE PROCEDURE sp_Prestamo_Calificacion_Prestamista(
	inPrestamo_Id BIGINT,
	inCalificacion BIGINT
)
BEGIN
	UPDATE Prestamo
	SET 
		CalificacionAlPrestamista = inCalificacion
	WHERE
		Id = inPrestamo_Id;
END
$$