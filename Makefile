#_BACKEND:=vulkan
_BACKEND:=opengl
#_BACKEND:=gles
_EXT:=a
_PRE:=lib
ifeq ($(OS),Windows_NT)
    _EXT=dll
	_PRE=
endif




all: clean deps assets build

.PHONY: clean
clean:
	@go clean && rm -rf build

.PHONY: assets
assets:
	@go generate ./..

.PHONY: deps
deps:
	@go mod tidy

.PHONY: build
build:
	@echo Building using $(_BACKEND)
	@CGO_ENABLED=1 go build -buildmode=archive -x -tags $(_BACKEND) -o build/$(_PRE)reactor-$(_BACKEND).$(_EXT) github.com/reactor3d/reactor

.PHONY: docs
docs:
	@godoc