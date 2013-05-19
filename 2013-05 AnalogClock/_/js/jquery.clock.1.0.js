/**
 * Copyright (c) 2013, Thomas Charrière
 * http://www.codepanda.ch/
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
 * and associated documentation files (the "Software"), to deal in the Software without restriction,
 * including without limitation the rights to use, copy, modify, merge, publish, distribute,
 * sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is 
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or
 * substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE
 * AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
(function ($) {

    $.fn.clock = function () {

        var fn = {
            easing : function (x, t, b, c, d) { return b + (t / d) * c; } // linear
        };

        return this.each(function () {
            var $t = $(this);

            // fix size of clock, should be square
            if ($t.width() != $t.height()) {
                if ($t.width() > $t.height()) {
                    $t.height($t.width());
                } else {
                    $t.width($t.height())
                }
            }

            $t
                .append('<div class="face"><div class="inner"></div></div>')
                .append('<div class="hand hour"></div>')
                .append('<div class="hand minute"></div>')
                .append('<div class="hand second"></div>');

            var $face = $t.find('.face'), $hour = $t.find('.hour'), $min = $t.find('.minute'), $sec = $t.find('.second');

            for (var i = 0; i < 12; i++) {
                var $tick = $('<span class="tick"></span>');
                $face.append($tick);
                $tick
                    .css({ 
                        'left' : $t.width() / 2 - $tick.width() / 2 + 'px',
                        'transformOrigin' : '50% ' + ($t.height() / 2) + 'px'
                    })
                    .rotate({ angle: 360 * i / 12 });
            }

            var data;

            if ($t.attr('data-time') != undefined) {
                data = $t.attr('data-time').split(':');
            } else {
                // ok get local time
                var currentDate = new Date();
                data = [currentDate.getHours(), currentDate.getMinutes(), currentDate.getSeconds()];
            }

            var aHour = 360 * parseInt(data[0]) / 12,
                aMin = 360 * parseInt(data[1]) / 60,
                aSec = 360 * parseInt(data[2]) / 60;

            var anis = {
                hour: {
                    duration: 43200000,
                    animateTo: aHour,
                    easing: fn.easing,
                    callback: function () { anis.hour.animateTo += 360; $hour.rotate(anis.hour); }
                },
                min: {
                    duration: 3600000,
                    animateTo: aMin,
                    easing: fn.easing,
                    callback: function () { anis.min.animateTo += 360; $min.rotate(anis.min); }
                },
                sec: {
                    duration: 60000,
                    animateTo: aSec,
                    easing: fn.easing,
                    callback: function () { anis.sec.animateTo += 360; $sec.rotate(anis.sec); }
                }
            };

            $hour
                .css('top', ($t.height() / 2 - $hour.height()) + 'px')
                .rotate({ angle: aHour });
            anis.hour.callback();

            $min
                .css('top', ($t.height() / 2 - $min.height()) + 'px')
                .rotate({ angle: aMin });
            anis.min.callback();

            $sec
                .css('top', ($t.height() / 2 - $sec.height()) + 'px')
                .rotate({ angle: aSec });
            anis.sec.callback();
        });

    };

}(jQuery));