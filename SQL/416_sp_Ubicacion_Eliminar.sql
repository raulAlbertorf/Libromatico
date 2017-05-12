--DELIMITER $$
DROP PROCEDURE IF EXISTS sp_Ubicacion_Eliminar $$

CREATE PROCEDURE sp_Ubicacion_Eliminar (
  	inId BIGINT
)
BEGIN
	delete FROM Ubicacion
    WHERE
		Perfil_Id = inId;
END
$$