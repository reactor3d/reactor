package reactor

type RVertex3D struct {
	Position RVector3
	Normal   RVector3
	Binormal RVector3
	TexCoord RVector2
}
type RVertex2D struct {
	Position RVector2
	TexCoord RVector2
}
type RIVertexData interface {
	Sizeof() uint64
}
type RVertexBuffer struct{}
type RIndexBuffer struct{}
type RGeometryBuffer struct{}
