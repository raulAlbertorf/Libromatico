--DELIMITER $$
DROP PROCEDURE IF EXISTS sp_Item_Crear $$

CREATE PROCEDURE sp_Item_Crear(
	inTitulo VARCHAR(256),
	inResumen TEXT,
    inVeces_Visto BIGINT,
    inPerfil_Id BIGINT,
    inUrlImagen VARCHAR(256),
    OUT outId BIGINT
)
BEGIN
	INSERT INTO Item
		(Titulo, Resumen, VecesVisto, Perfil_Id, Disponibilidad,UrlImagen)
	VALUES
		(inTitulo, inResumen, inVeces_Visto, inPerfil_Id, TRUE, inUrlImagen);
	SET outId = LAST_INSERT_ID();
END
$$
