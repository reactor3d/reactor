package reactor

type RConfiguration interface {
	SetVSync(bool)
	GetVSync() bool
	SetWindowMode(RWindowMode)
	GetWindowMode() RWindowMode
	GetWindowSize() RSize
	SetWindowSize(RSize)
	SetWindowTitle(string)
	GetWindowTitle() string
	SetServerMode(bool)
	GetServerMode() bool
	SetRenderingBackend(RRenderingBackend)
	GetRenderingBackend() RRenderingBackend
}
type rconfiguration struct {
	vsync   bool
	mode    RWindowMode
	size    RSize
	title   string
	server  bool
	backend RRenderingBackend
}

func DefaultConfiguration() RConfiguration {
	return rconfiguration{
		vsync:   false,
		mode:    RWINDOWMODE_WINDOW,
		size:    RSize{640, 480},
		title:   "Reactor 3D",
		server:  false,
		backend: RRENDERINGBACKEND_VULKAN,
	}
}
func NewConfiguration(vsync bool, mode RWindowMode, size RSize, title string, serverMode bool, backend RRenderingBackend) RConfiguration {
	return rconfiguration{
		vsync,
		mode,
		size,
		title,
		serverMode,
		backend,
	}
}
func (r rconfiguration) GetVSync() bool {
	return r.vsync
}
func (r rconfiguration) GetWindowMode() RWindowMode {
	return r.mode
}
func (r rconfiguration) GetWindowSize() RSize {
	return r.size
}
func (r rconfiguration) GetWindowTitle() string {
	return r.title
}
func (r rconfiguration) GetServerMode() bool {
	return r.server
}
func (r rconfiguration) GetRenderingBackend() RRenderingBackend {
	return r.backend
}
func (r rconfiguration) SetVSync(enable bool) {
	r.vsync = enable
}
func (r rconfiguration) SetWindowMode(mode RWindowMode) {
	r.mode = mode
}
func (r rconfiguration) SetWindowSize(size RSize) {
	r.size = size
}
func (r rconfiguration) SetWindowTitle(title string) {
	r.title = title
}
func (r rconfiguration) SetServerMode(server bool) {
	r.server = server
}
func (r rconfiguration) SetRenderingBackend(backend RRenderingBackend) {
	r.backend = backend
}
