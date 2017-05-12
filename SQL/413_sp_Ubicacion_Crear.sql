--DELIMITER $$
DROP PROCEDURE IF EXISTS sp_Ubicacion_Crear $$

CREATE PROCEDURE sp_Ubicacion_Crear (
	inLatitude FLOAT( 10, 6 ),
  	inLongitude FLOAT( 10, 6 ),
  	inPerfil_Id BIGINT
)
BEGIN
	INSERT INTO Ubicacion
	(
		Latitude,
		Longitude,
		Perfil_Id
	)
	VALUES
	(
		inLatitude,
		inLongitude,
		inPerfil_Id
	);
END
$$