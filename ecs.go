package reactor

import "reflect"

type REntity interface {
	RDestroyable
	RClonable
	ID() RID
	GetComponent(t reflect.Type) (RComponent, error)
	AddComponent(RComponent) error
	RemoveComponent(RComponent) error
	Update(RGameTime) error
	Render() error
}

type RComponent interface {
	RDestroyable
	ID() RID
	Update(RGameTime) error
}

type RBehavior interface {
	RDestroyable
	Init() error
	GetEntity() REntity
	Activated()
	Deactivated()
	Update(RGameTime) error
}
