--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Perfil_Reputacion_Prestamista $$
CREATE PROCEDURE sp_Perfil_Reputacion_Prestamista (
	inId BIGINT
)
BEGIN
	SELECT 	
		AVG(CalificacionAlPrestamista) AS Reputacion
	FROM 
		Prestamo
	WHERE 
		Prestamista_Id = inId
	GROUP BY
		Prestamista_Id;
END $$