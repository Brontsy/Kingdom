




	function GameWorld(container, options) {
		return this.initialize(container, options);
	}

	$.fn.gameWorld = function (options) {

		return new GameWorld(this, options);
	};


	GameWorld.prototype = {

	    _offset: { x: 0, y: 0 },
	    _originalPosition: { x: 0, y: 0 },
		_tileWidth: 100,
		_tileHeight: 65,
        _zoomLevel: 1,

		_windowWidth: null,
        _windowHeight: null,

        _container: null,

        _regionIds: new Array(),

		initialize: function (container, options) {

			var $this = this;

			this._container = container;
			this._offset = options.offset || { x: 0, y: 0 };
			this._originalPosition = options.offset || { x: 0, y: 0 };

			this._zoomLevel = options.zoomLevel || 1;
			this._tileWidth = options.tileWidth || 100;
			this._tileHeight = options.tileHeight || 100;
			
			this._tileWidth = this._tileWidth * this._zoomLevel;
			this._tileHeight = this._tileHeight * this._zoomLevel;

			this._windowWidth = $(window).width();
			this._windowHeight = $(window).height();

			this._container.draggable({

			    drag: function () {

			    },
			    stop: function (event, ui) { $this.stopDrag(event, ui); }
			});

			$.each($('.region'), function (index, element) {
			    $this._regionIds.push($(this).data('region-id'));
			});

			$('.region > div').on('click', function (event) {

			    var testPos = $this.getTileXY(event.pageX, event.pageY);
			    $('.update-click').html('MouseX: ' + event.pageX + ' MouseY: ' + event.pageY +  ' || X: ' + testPos.x + ' Y:' + testPos.y);
			});

			$('.drag').bind('mousewheel', function (event, delta) { $this.zoom(event); });

		},

		zoom: function(event)
		{
		    $('.drag').removeClass('zoom-' + this._zoomLevel);

		    this._zoomLevel += event.originalEvent.wheelDelta / 120;

		    $('.drag').addClass('zoom-' + this._zoomLevel);

		    $('.drag').children().remove();

		    this._tileWidth = 100 * this._zoomLevel;
		    this._tileHeight = 100 * this._zoomLevel;

		    this._regionIds = new Array();
		    this.getVisibleRegions();
		},

		getTileXY: function (isoX, isoY) {

		    var position = this.isoTo2D(isoX, isoY);

			var tilPos = {};
			tilPos.x = Math.floor(position.x / (this._tileWidth / 2));
			tilPos.y = Math.floor(position.y / (this._tileHeight / 2));

			return tilPos;
		},

		isoTo2D: function (x, y) {

			x += this._offset.x;
			y += this._offset.y;

			var position = {};
			position.x = ((2 * y + x) / 2);
			position.y = ((2 * y - x) / 2);

			return position;
		},

		stopDrag: function (event, ui) {

		    this._offset.x = this._originalPosition.x + ui.position.left * -1;
		    this._offset.y = this._originalPosition.y + ui.position.top * -1;

		    this.getVisibleRegions();

		    //var regions = this.getBorderRegions();

		    //this.getRegions(regions);
		},

		getBorderRegions: function()
		{
		    var topLeft = { x: 0, y: 0 };
		    var topRight = { x: this._windowWidth, y: 0 };
		    var bottomLeft = { x: 0, y: this._windowHeight };;
		    var bottomRight = { x: this._windowWidth, y: this._windowHeight };;

		    var regions = {} 
		    
		    regions.topLeft = this.getTileXY(topLeft.x, topLeft.y), 
            regions.topRight = this.getTileXY(topRight.x, topRight.y), 
            regions.bottomLeft = this.getTileXY(bottomLeft.x, bottomLeft.y), 
            regions.bottomRight = this.getTileXY(bottomRight.x, bottomRight.y)
		    
            return regions;
		},

		tilePositionToRegionPosition: function(position)
		{
		    var newPos = {}
		    newPos.x = Math.floor(position.x / 10);
		    newPos.y = Math.floor(position.y / 10);

		    return newPos;
		},

		getRegion: function (x, y) {

		    $this = this;

		    $.ajax({
		        url: '/region/get/' + x + '-' + y,
		        method: 'GET',
		        success: function (response)
		        {
		            $(response).appendTo($this._container);
		        }
		    });
		},

		getRegions: function (regions) {

		    $this = this;

		    var topLeft = this.tilePositionToRegionPosition(regions.topLeft);
		    var topRight = this.tilePositionToRegionPosition(regions.topRight);
		    var bottomLeft = this.tilePositionToRegionPosition(regions.bottomLeft);
		    var bottomRight = this.tilePositionToRegionPosition(regions.bottomRight);


		    $('.updatetl').html(topLeft.x + ' : ' + topLeft.y);
		    $('.updatetr').html(topRight.x + ' : ' + topRight.y);
		    $('.updatebl').html(bottomLeft.x + ' : ' + bottomLeft.y);
		    $('.updatebr').html(bottomRight.x + ' : ' + bottomRight.y);

		    var x = topLeft.x < bottomLeft.x ? topLeft.x : bottomLeft.x;
		    var y = topLeft.x < bottomLeft.x ? topLeft.x : bottomLeft.x;

		    $.ajax({
		        url: '/region/get/range',
		        contentType: 'application/json',
		        data: JSON.stringify({ topLeftX: topLeft.x, topLeftY: topLeft.y, topRightX: topRight.x, topRightY: topRight.y, bottomLeftX: bottomLeft.x, bottomLeftY: bottomLeft.y, bottomRightX: bottomRight.x, bottomRightY: bottomRight.y, exclude: $this._regionIds }),
		        method: 'POST',
		        success: function (response) {
		            $(response).appendTo($this._container);

		            $('.region > div').on('click', function (event) {

		                var testPos = $this.getTileXY(event.pageX, event.pageY);
		                $('.update-click').html('MouseX: ' + event.pageX + ' MouseY: ' + event.pageY + ' || X: ' + testPos.x + ' Y:' + testPos.y);
		            });

		        }
		    });
		},

	    getVisibleRegions: function (x, y) {

	        $this = this;

	        $.ajax({
	            url: '/region/get/visible',
	            contentType: 'application/json',
	            data: JSON.stringify({ screenWidth: $this._windowWidth, screenHeight: $this._windowHeight, x: this._offset.x, y: this._offset.y, exclude: $this._regionIds, zoomLevel: $this._zoomLevel }),
	            method: 'POST',
	            success: function (response) {
	                $(response).appendTo($this._container);

	                $('.region > div').on('click', function (event) {

	                    var testPos = $this.getTileXY(event.pageX, event.pageY);
	                    $('.update-click').html('MouseX: ' + event.pageX + ' MouseY: ' + event.pageY + ' || X: ' + testPos.x + ' Y:' + testPos.y);
	                });
	                $.each($('.region'), function (index, element) {
	                    $this._regionIds.push($(this).data('region-id'));
	                
	                });

	                $this.removeNonVisibleRegions();
	            }
	        });
	    },

	    removeNonVisibleRegions: function()
	    {
	        var ids;
	        $.ajax({
	            url: '/region/get/visible/ids',
	            contentType: 'application/json',
	            dataType: "json",
	            data: JSON.stringify({ screenWidth: $this._windowWidth, screenHeight: $this._windowHeight, x: this._offset.x, y: this._offset.y }),
	            method: 'POST',
	            success: function (response) {
	                
	                ids = response;

	                $.each($('.region'), function (index, element) {

	                    var inArray = jQuery.inArray(parseInt($(element).data('region-id')), ids);

	                    if (inArray == -1) {
	                        $(element).remove();
	                    }
	                });

	                $this._regionIds = new Array();
	                $.each($('.region'), function (index, element) {
	                    $this._regionIds.push($(this).data('region-id'));
	                });

	                $('.updatetl').html($('.region').length);
	            }
	        });
        }
};
