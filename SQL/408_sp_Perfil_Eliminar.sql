--DELIMITER $$

DROP PROCEDURE IF EXISTS sp_Perfil_Eliminar $$

CREATE PROCEDURE sp_Perfil_Eliminar(
	inId BIGINT UNSIGNED
)
BEGIN
	DELETE FROM 
		Perfil 
	WHERE
		Id = inId;
END
$$