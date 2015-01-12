#!/bin/sh

mdoc update -i Reactor/bin/x64/Debug/Reactor.xml -o docs/en Reactor/bin/x64/Debug/Reactor.dll
mdoc export-html -o docs/html/en docs/en