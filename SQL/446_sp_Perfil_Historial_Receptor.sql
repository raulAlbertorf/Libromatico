--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Perfil_Historial_Receptor $$

CREATE PROCEDURE sp_Perfil_Historial_Receptor (
	inPerfil_Id BIGINT,
	inOffset INT,
	inLimit INT
)
BEGIN
	SELECT
		P.Id,
		P.Estado,
		P.FechaEnvio,
		P.FechaRecepcion,
		P.FechaExpiracion,
		P.CalificacionAlReceptor,
		P.CalificacionAlPrestamista,
		P.UltimaModificacion,
		
		I.Id AS Item_Id,
		I.Titulo,
		I.UrlImagen AS Item_UrlImagen,

		PE.Id AS Perfil_Id,
		PE.Nombre,
		PE.UrlImagen AS Perfil_UrlImagen

	FROM Prestamo AS P
	INNER JOIN Item AS I
		ON P.Item_Id = I.Id
	INNER JOIN Perfil AS PE
		ON P.Prestamista_Id = PE.Id
	WHERE
		P.Receptor_Id = inPerfil_Id
        ORDER BY P.UltimaModificacion DESC
	LIMIT
		inOffset, inLimit;
END
$$