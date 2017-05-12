--DELIMITER $$
DROP PROCEDURE IF EXISTS sp_Prestamo_Eliminar $$

CREATE PROCEDURE sp_Prestamo_Eliminar(
	inId BIGINT UNSIGNED
)
BEGIN
	DELETE FROM 
		Prestamo 
	WHERE
		Id = inId;
END
$$