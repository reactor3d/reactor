package reactor

type RShader interface{}
type RMaterial interface {
	SetShader(RShader) error
	GetShader() (RShader, error)
	SetAlbedo(RTexture) error
}
