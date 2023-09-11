package reactor

import "fmt"

type RConsole interface {
	Log(string, ...interface{})
}

type rconsole struct {
	RConsole
}

var (
	console *rconsole = &rconsole{}
)

func (c *rconsole) Log(format string, args ...interface{}) {
	fmt.Printf(format, args...)
}
