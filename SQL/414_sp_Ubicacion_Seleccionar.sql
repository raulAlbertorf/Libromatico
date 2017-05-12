--DELIMITER $$
DROP PROCEDURE IF EXISTS sp_Ubicacion_Seleccionar $$

CREATE PROCEDURE sp_Ubicacion_Seleccionar (
  	inId BIGINT
)
BEGIN
	SELECT 
		Latitude,
        Longitude,
        Perfil_Id
    FROM
		Ubicacion
    WHERE
		Perfil_Id = inId;
END
$$