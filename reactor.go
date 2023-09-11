package reactor

import (
	"runtime"
	"time"

	"github.com/go-gl/glfw/v3.3/glfw"
	"github.com/reactor3d/reactor/platform/opengl"
	"github.com/reactor3d/reactor/platform/vulkan"
)

type REngine interface {
	Configure(config RConfiguration) error
	CreateScene() RScene
	Run() error
	Destroy() error
}

type RGameTime interface {
	GetElapsed() int64
	GetDelta() float64
	GetStartTime() *time.Time
	GetNow() *time.Time
}
type rgametime struct {
	start *time.Time
	delta float64
}

func (g *rgametime) GetElapsed() int64 {
	return time.Now().UnixNano() - g.start.UnixNano()
}
func (g *rgametime) GetDelta() float64 {
	return float64(g.delta)
}
func (g *rgametime) GetStartTime() *time.Time {
	return g.start
}
func (g *rgametime) GetNow() *time.Time {
	now := time.Now()
	return &now
}

type rengine struct {
	REngine
	window   *glfw.Window
	gameTime *rgametime
}

var (
	_engine *rengine
)

//export Reactor_NewEngine
func NewEngine() REngine {
	// lock the thread so that GLFW/Vulkan/OpenGL execute on the main os thread.
	runtime.LockOSThread()
	if _engine == nil {
		now := time.Now()
		_engine = &rengine{gameTime: &rgametime{start: &now, delta: 0.0}}
		runtime.SetFinalizer(_engine, func(o REngine) { o.Destroy() })
	}
	return _engine
}

//export Reactor_Engine_Configure
func (e *rengine) Configure(config RConfiguration) error {
	err := glfw.Init()
	if err != nil {
		return err
	}
	size := config.GetWindowSize()
	mode := config.GetWindowMode()
	backend := config.GetRenderingBackend()

	var monitor *glfw.Monitor
	if backend == RRENDERINGBACKEND_NONE || backend == RRENDERINGBACKEND_VULKAN {
		glfw.WindowHint(glfw.ClientAPI, glfw.NoAPI)
	}
	if backend == RRENDERINGBACKEND_OPENGL {
		glfw.WindowHint(glfw.ClientAPI, glfw.OpenGLAPI)
		glfw.WindowHint(glfw.ContextVersionMajor, 4)
		glfw.WindowHint(glfw.ContextVersionMinor, 1)
		glfw.WindowHint(glfw.OpenGLProfile, glfw.OpenGLCoreProfile)
		glfw.WindowHint(glfw.OpenGLForwardCompatible, glfw.True)
	}
	if backend == RRENDERINGBACKEND_GLES {
		glfw.WindowHint(glfw.ClientAPI, glfw.OpenGLESAPI)
		glfw.WindowHint(glfw.ContextVersionMajor, 3)
		glfw.WindowHint(glfw.ContextVersionMinor, 0)
		glfw.WindowHint(glfw.OpenGLProfile, glfw.OpenGLAnyProfile)
		glfw.WindowHint(glfw.OpenGLForwardCompatible, glfw.True)
	}

	glfw.WindowHint(glfw.Resizable, glfw.False)

	switch mode {
	case RWINDOWMODE_BORDERLESS:
		glfw.WindowHint(glfw.Maximized, glfw.True)
		glfw.WindowHint(glfw.Decorated, glfw.False)
	case RWINDOWMODE_FULLSCREEN:
		monitor = glfw.GetPrimaryMonitor()
	}

	_engine.window, err = glfw.CreateWindow(size.Width, size.Height, config.GetWindowTitle(), monitor, nil)
	if err != nil {
		panic(err)
	}
	if backend == RRENDERINGBACKEND_VULKAN {
		vulkan.VkInit(config.GetServerMode())
	}
	if backend == RRENDERINGBACKEND_OPENGL {
		opengl.Init(config.GetServerMode())
	}
	_engine.window.MakeContextCurrent()
	return nil
}

//export Reactor_Engine_CreateScene
func (e *rengine) CreateScene() RScene {
	return nil
}

//export Reactor_Engine_Run
func (e *rengine) Run() error {
	return nil
}

//export Reactor_Engine_Destroy
func (e *rengine) Destroy() error {
	e.window.Destroy()
	glfw.Terminate()
	runtime.UnlockOSThread()
	return nil
}
