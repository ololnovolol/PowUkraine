import React, { useEffect, useState } from 'react';
import { Map, TileLayer, Marker, Popup } from 'react-leaflet';
import axios from 'axios';
import Basemap from '../maps/BaseMaps';

function AllMarks() {
    const [basemap, setBasemap] = useState('osm');
    const [marks, setMarks] = useState([]);

    useEffect(() => {
        const loadMarks = async () => {
            const res = await axios.get(`https://localhost:44312/api/marks`);
            setMarks(res.data);
        };
        loadMarks();
    }, []);

    const position = [48.96, 32.56];
    const basemapsDict = {
        osm: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
        hot: 'https://{s}.tile.openstreetmap.fr/hot/{z}/{x}/{y}.png',
        cycle: 'https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}',
        dark: 'https://{s}.basemaps.cartocdn.com/dark_all/{z}/{x}/{y}{r}.png',
    };

    return (
        <Map className="map" center={position} zoom={6}>
            <TileLayer
                attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                url={basemapsDict[basemap]}
            />
            <Basemap basemap={basemap} onChange={setBasemap} />

            {marks.map(mark => {
                const coords = [mark.latitude, mark.longitude];

                return (
                    <Marker key={mark.id} position={coords}>
                        <Popup>
                            <div>
                                <h4>{mark.title}</h4>
                                <span Style={'font-size: 2rem'}>
                                    {mark.description}
                                </span>
                            </div>
                        </Popup>
                    </Marker>
                );
            })}
        </Map>
    );
}

export default AllMarks;
