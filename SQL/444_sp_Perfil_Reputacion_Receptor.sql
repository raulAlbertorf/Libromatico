--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Perfil_Reputacion_Receptor $$
CREATE PROCEDURE sp_Perfil_Reputacion_Receptor (
	inId BIGINT
)

BEGIN
	SELECT 	
		AVG(CalificacionAlReceptor) AS Reputacion
	FROM 
		Prestamo
	WHERE 
		Receptor_Id = inId
	GROUP BY
		Receptor_Id;
END $$