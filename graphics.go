package reactor

type RGraphicsContext interface {
	SetVertexBuffer(RVertexBuffer) error
	SetIndexBuffer(RIndexBuffer) error
	Draw() error
	DrawInstanced() error
	SetShader(int, RShader) error
	GetShader(int, RShader) error
	SetBlendMode(RBlendMode)
	GetBlendMode() RBlendMode
	SetAlphaToCoverage(bool)
	GetAlphaToCoverage() bool
	Present() error
}

type RBuffer interface {
}

func newGraphicsContext() RGraphicsContext {
	return nil
}
