import React from 'react';
import L from 'leaflet';
import { Map, TileLayer, Marker, Popup } from 'react-leaflet';
import Basemap from './BaseMaps';
import '../../style/maps/map.css';
import { Link } from 'react-router-dom';

L.Icon.Default.imagePath = 'https://unpkg.com/leaflet@1.5.0/dist/images/';

class MapComponent extends React.Component {
    state = {
        lat: 50.45466,
        lng: 30.5238,
        zoom: 10,
        basemap: 'osm',
        markers: [[0, 0]],

        geojsonvisible: false,
        visibleModal: false,
    };

    addMarker = e => {
        const { markers } = this.state;
        if (markers.length > 0) {
            markers[0] = e.latlng;
        } else {
            markers.push(e.latlng);
        }
        this.setState({ markers });
    };

    onCoordInsertChange = (lat, long, z) => {
        this.setState({
            lat: lat,
            lng: long,
            zoom: z,
        });
    };

    onBMChange = bm => {
        this.setState({
            basemap: bm,
        });
    };

    onGeojsonToggle = e => {
        this.setState({
            geojsonvisible: e.currentTarget.checked,
        });
    };

    routingFunction = () => {
        this.props.history.push({
            pathname: `/massage`,
        });
    };

    render() {
        var center = [this.state.lat, this.state.lng];
        var z = this.state.zoom;

        const basemapsDict = {
            osm: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
            hot: 'https://{s}.tile.openstreetmap.fr/hot/{z}/{x}/{y}.png',
            cycle: 'https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}',
            dark: 'https://{s}.basemaps.cartocdn.com/dark_all/{z}/{x}/{y}{r}.png',
        };

        return (
            <Map zoom={z} center={center} onClick={this.addMarker}>
                <TileLayer
                    attribution='&amp;copy <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
                    url={basemapsDict[this.state.basemap]}
                />
                <Basemap
                    basemap={this.state.basemap}
                    onChange={this.onBMChange}
                />

                {this.state.markers.map((position, idx) => (
                    <>
                        <Marker
                            key={`marker-${idx}`}
                            position={position}
                            on={this.routingFunction}>
                            <Popup>
                                <span>
                                    <Link
                                        to={{
                                            pathname: 'message',
                                            state: this.state,
                                        }}>
                                        Latitude: {position['lat']}
                                        <br />
                                        Longitude: {position['lng']}
                                        <br />
                                    </Link>
                                </span>
                            </Popup>
                        </Marker>
                    </>
                ))}
            </Map>
        );
    }
}

export default MapComponent;
