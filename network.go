package reactor

import (
	"io"
	"net"
)

type RNetworkClient interface {
	io.ReadWriteSeeker

	GetID() int64
	GetAddr() net.Addr
	GetEntity() REntity
}

type RNetworkServer interface{}

type RPacket interface {
	GetType() int64
	SetType(t int64)
	GetData() ([]byte, error)
	SetData(data []byte) error
}
