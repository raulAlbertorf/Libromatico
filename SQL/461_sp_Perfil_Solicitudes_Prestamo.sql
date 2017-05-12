--DELIMITER $$
DROP PROCEDURE IF EXISTS sp_Perfil_Solicitudes_Prestamo $$

CREATE PROCEDURE sp_Perfil_Solicitudes_Prestamo(
	inPerfil_Id BIGINT
)
BEGIN
	SELECT
		P.Id AS Prestamo_Id,
		P.Estado,
		P.FechaEnvio,
		P.FechaRecepcion,
		P.FechaExpiracion,
		P.CalificacionAlReceptor,
		P.CalificacionAlPrestamista,
		P.UltimaModificacion,

		I.Id AS Item_Id,
		I.Titulo AS Item_Titulo,
		I.UrlImagen AS Item_UrlImagen,

		PE.Id AS Perfil_Id,
		PE.Nombre AS Perfil_Nombre,
		PE.UrlImagen AS Perfil_UrlImagen

	FROM
		Prestamo AS P
	INNER JOIN Item AS I
		ON P.Item_Id = I.Id
	INNER JOIN Perfil AS PE
		ON PE.Id = P.Receptor_Id
	WHERE P.Prestamista_Id = inPerfil_Id AND P.Estado = 1;

END
$$