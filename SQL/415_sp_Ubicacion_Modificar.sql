--DELIMITER $$
DROP PROCEDURE IF EXISTS sp_Ubicacion_Modificar $$

CREATE PROCEDURE sp_Ubicacion_Modificar (
	inLatitude FLOAT( 10, 6 ),
  	inLongitude FLOAT( 10, 6 ),
  	inPerfil_Id BIGINT
)
BEGIN
	UPDATE Ubicacion
	SET
		Latitude  = inLatitude,
		Longitude = inLongitude
 	WHERE
		Perfil_Id = inPerfil_Id;
END
$$