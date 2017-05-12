--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Cuenta_Seleccionar_Perfiles $$
CREATE PROCEDURE sp_Cuenta_Seleccionar_Perfiles (
	inEmail VARCHAR(256)
)
BEGIN
	SELECT
		Perfil.Id AS Id,
		Perfil.Nombre AS Nombre,
		Perfil.UrlImagen AS UrlImagen,
		Perfil.Nacionalidad AS Nacionalidad,
		Perfil.Cuenta_Email AS Cuenta_Email,
		Ubicacion.Latitude AS Lat,
		Ubicacion.Longitude AS Lon
	FROM 
		Perfil 
	JOIN 
		Ubicacion 
	ON 
		Perfil.Id = Ubicacion.Perfil_Id
	WHERE
		Perfil.Cuenta_Email = inEmail;
END $$


                        